import BaseButton from '@/core/components/buttons/BaseButton';
import BaseInput from '@/core/components/common/BaseInput';
import SkeletonLoader from '@/core/components/common/BaseSkeleton';
import { Icons } from '@/core/icons';
import useMergeState from '@/hooks/useMergeState';
import useLocaleStore from '@/stores/localeStore';
import { ApiResponse, PaginatedList } from '@/types/auth';
import { Entity } from '@/types/core';
import { ApiUtil } from '@/utils/apiUtil';
import { useQuery } from '@tanstack/react-query';
import { Table, TablePaginationConfig, TableProps } from 'antd';
import { t } from 'i18next';
import _ from 'lodash';
import React, {
    forwardRef,
    useCallback,
    useEffect,
    useImperativeHandle,
    useMemo,
    useRef,
    useState
} from 'react';

interface ToolbarConfig {
    search?: boolean;
    create?: boolean;
    onCreate?: () => void;
}

interface PaginationGrid extends Omit<TablePaginationConfig, 'pageSize' | 'current'> {
    pageSize: number;
    current: number;
}

interface ApiProps {
    url?: string;
    params?: Record<string, string>;
    pageSize?: number;
}

interface BaseGridProps<T extends Entity> extends Omit<TableProps<T>, 'pagination' | 'loading'> {
    gridKey: string;
    columns: TableProps<T>['columns'];
    pagination?: boolean;
    toolbarConfig?: ToolbarConfig;
    api?: ApiProps;
    indexColumn?: boolean;
}

export interface BaseGridRef {
    reload: () => void;
}

const BaseGrid = <T extends Entity>(
    {
        columns = [],
        rowKey,
        pagination = true,
        toolbarConfig,
        api = { pageSize: 10 },
        gridKey,
        indexColumn = true,
        ...restProps
    }: BaseGridProps<T>,
    ref: React.Ref<BaseGridRef>
) => {
    const containerRef = useRef<HTMLDivElement>(null);
    const [scrollState, setScrollState] = useMergeState<TableProps<T>['scroll']>({});
    const [paginationState, setPaginationState] = useMergeState<PaginationGrid>({
        pageSize: api?.pageSize || 10,
        current: 1
    });
    const [searchKey, setSearchKey] = useState<string>('');
    const { locale } = useLocaleStore();

    const fetchData = async () => {
        try {
            const response = await ApiUtil.Axios<ApiResponse<PaginatedList<T>>>(
                'get',
                api?.url ?? '',
                {
                    ...api?.params,
                    searchKey,
                    limit: paginationState.pageSize,
                    offset: paginationState.current
                }
            );
            return response?.data;
        } catch (error) {
            console.error('Error fetching data:', error);
            throw error;
        }
    };

    const { data, isLoading, refetch } = useQuery({
        queryKey: [gridKey, api?.url, paginationState.pageSize, paginationState.current, searchKey],
        queryFn: fetchData,
        enabled: !!api?.url
    });

    useImperativeHandle(ref, () => ({
        reload: refetch
    }));

    const total = data?.result?.total || 0;
    const start = (paginationState.current - 1) * paginationState.pageSize + 1;
    const end = Math.min(paginationState.current * paginationState.pageSize, total);

    const handlePageChange = (page: number, pageSize?: number) => {
        setPaginationState({ current: page, pageSize: pageSize || paginationState.pageSize });
    };

    const calculateScroll = useCallback(() => {
        if (containerRef.current) {
            const tbody = containerRef.current.querySelector('.ant-table-tbody') as HTMLDivElement;
            const pagination = containerRef.current.querySelector(
                '.ant-table-pagination'
            ) as HTMLDivElement;
            const tHeader = containerRef.current.querySelector(
                '.ant-table-thead'
            ) as HTMLDivElement;

            const containerHeight = containerRef.current.clientHeight;
            const tbodyHeight = tbody?.scrollHeight || 0;
            const paginationHeight = pagination?.offsetHeight || 0;
            const tHeaderHeight = tHeader?.clientHeight || 0;

            let topBottomHeight = paginationHeight * 2 + tHeaderHeight;
            if (toolbarConfig) {
                const toolbar = containerRef.current.querySelector(
                    '.base-table-toolbar'
                ) as HTMLDivElement;
                topBottomHeight += toolbar?.clientHeight || 0;
            }

            setScrollState({
                y:
                    containerHeight < tbodyHeight + topBottomHeight
                        ? containerHeight - topBottomHeight
                        : undefined
            });
        }
    }, [toolbarConfig, setScrollState]);

    useEffect(() => {
        calculateScroll();
        const debouncedResizeHandler = _.debounce(calculateScroll, 300);
        window.addEventListener('resize', debouncedResizeHandler);

        return () => window.removeEventListener('resize', debouncedResizeHandler);
    }, [calculateScroll]);

    const debouncedSearch = useCallback(
        _.debounce((value: string) => {
            setSearchKey(value);
            setPaginationState({ current: 1 });
            refetch();
        }, 300),
        []
    );

    const handleSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
        debouncedSearch(event?.target?.value);
    };

    const Toolbar = useMemo(() => {
        if (!toolbarConfig) return null;
        return (
            <div className="base-table-toolbar p-6 pb-3 flex justify-between">
                {toolbarConfig.search && (
                    <BaseInput
                        placeholder={t('table.search')}
                        className="py-1 px-3 w-60"
                        allowClear
                        onChange={handleSearch}
                    />
                )}
                <div>
                    {toolbarConfig.create && (
                        <BaseButton
                            variants="primary"
                            icon={<Icons.PlusCircle />}
                            onClick={toolbarConfig.onCreate}
                        >
                            {t('button.create')}
                        </BaseButton>
                    )}
                </div>
            </div>
        );
    }, [toolbarConfig]);

    const DisplayItems = useMemo(() => {
        if (!total) return null;
        return (
            <div className="absolute bottom-0 flex gap-1 p-4 text-main-tertiary dark:text-dark-main-tertiary items-center">
                {t('table.display', { start, end, total })}
            </div>
        );
    }, [total, start, end, locale]);

    const columnsWithIndex = useMemo(() => {
        if (indexColumn) {
            return [
                {
                    title: t('table.no'),
                    key: 'index',
                    render: (_, __, index: number) =>
                        (paginationState.current - 1) * paginationState.pageSize + index + 1,
                    width: 50
                },
                ...columns
            ];
        }
        return columns;
    }, [columns, indexColumn]);

    return (
        <div ref={containerRef} className="relative w-full h-full flex flex-col">
            {Toolbar}

            {isLoading ? (
                <SkeletonLoader />
            ) : (
                <Table
                    className="base-table w-full h-full flex-1"
                    dataSource={data?.result?.items}
                    columns={columnsWithIndex}
                    rowKey={rowKey || ((record) => _.toString(record.id))}
                    pagination={
                        pagination
                            ? {
                                  ...paginationState,
                                  onChange: handlePageChange,
                                  showSizeChanger: true,
                                  locale: { items_per_page: '' },
                                  onShowSizeChange: handlePageChange
                              }
                            : false
                    }
                    scroll={scrollState}
                    {...restProps}
                />
            )}
            {pagination && DisplayItems}
        </div>
    );
};

BaseGrid.displayName = 'BaseGrid';

export default forwardRef(BaseGrid) as <T extends Entity>(
    props: BaseGridProps<T> & { ref?: React.Ref<BaseGridRef> }
) => JSX.Element;
