import BaseButton from '@/core/components/buttons/BaseButton';
import BaseInput from '@/core/components/common/BaseInput';
import { Icons } from '@/core/icons/icon';
import useMergeState from '@/hooks/useMergeState';
import { ApiResponse, PaginatedList } from '@/types/auth';
import { Entity } from '@/types/core';
import { ApiUtil } from '@/utils/apiUtil';
import { useQuery } from '@tanstack/react-query';
import { Divider, Table, TablePaginationConfig, TableProps } from 'antd';
import _ from 'lodash';
import React, { useCallback, useEffect, useRef } from 'react';

interface ToolbarConfig {
    search?: boolean;
    create?: boolean;
}

interface ApiProps {
    url?: string;
    params?: Record<string, string>;
    pageSize?: number;
}

interface BaseGridProps<T> extends Omit<TableProps<T>, 'pagination' | 'loading'> {
    gridKey: string;
    columns: TableProps<T>['columns'];
    pagination?: boolean;
    toolbarConfig?: ToolbarConfig;
    api?: ApiProps;
}

const BaseGrid = <T extends Entity>({
    columns,
    rowKey,
    pagination = true,
    toolbarConfig,
    api = {
        pageSize: 10
    },
    gridKey,
    ...restProps
}: BaseGridProps<T>) => {
    const containerRef = useRef<HTMLDivElement>(null);
    const [scrollState, setScrollState] = useMergeState<TableProps<T>['scroll']>({});
    const [paginationState, setPaginationState] = useMergeState<TablePaginationConfig>({
        pageSize: 15,
        current: 1
    });

    const fetchData = async () => {
        const response = await ApiUtil.Axios<ApiResponse<PaginatedList<T>>>('get', api?.url ?? '');
        return response?.data;
    };

    const { data, isLoading } = useQuery({
        queryKey: [gridKey, api?.url],
        queryFn: fetchData,
        enabled: !!api
    });

    const pageSize = paginationState.pageSize || 15;
    const current = paginationState.current || 1;
    const total = data?.result?.items?.length || 0;
    const start = (current - 1) * pageSize + 1;
    const end = Math.min(current * pageSize, total);

    const handlePageChange = (page: number) => {
        setPaginationState({ current: page });
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

            const containerHeight = containerRef.current?.clientHeight || 0;
            const tbodyHeight = tbody?.scrollHeight || 0;
            const paginationHeight = pagination?.offsetHeight || 0;
            const tHeaderHeight = tHeader?.clientHeight || 0;

            let topBottomHeight = paginationHeight * 2 + tHeaderHeight;
            if (toolbarConfig) {
                const toolbar = containerRef.current.querySelector(
                    '.base-table-toolbar'
                ) as HTMLDivElement;
                const toolbarHeight = toolbar?.clientHeight || 0;
                topBottomHeight += toolbarHeight;
            }

            if (containerHeight < tbodyHeight + topBottomHeight) {
                setScrollState({ y: containerHeight - topBottomHeight });
            } else {
                setScrollState({ y: undefined });
            }
        }
    }, [setScrollState]);

    useEffect(() => {
        calculateScroll();

        const debouncedResizeHandler = _.debounce(calculateScroll, 300);
        window.addEventListener('resize', debouncedResizeHandler);

        return () => {
            window.removeEventListener('resize', debouncedResizeHandler);
        };
    }, [calculateScroll]);

    const toolbarGrid = () => {
        if (toolbarConfig) {
            return (
                <div className="base-table-toolbar p-6 flex justify-between">
                    {toolbarConfig.search && (
                        <BaseInput placeholder="Search" className="py-1.5 px-3 w-60" />
                    )}
                    {toolbarConfig.create && (
                        <BaseButton variants="primary" icon={<Icons.PlusCircle />}>
                            Create
                        </BaseButton>
                    )}
                </div>
            );
        }
    };

    const displayItems = () => {
        if (!total) return null;
        return (
            <div className="absolute bottom-0 flex gap-1 p-4 text-slate-400">
                <span>Displaying</span>
                <span>{start}</span>
                <span>to</span>
                <span>{end}</span>
                <span>of</span>
                <span>{total}</span>
                <span>entries</span>
            </div>
        );
    };

    return (
        <React.Fragment>
            <div ref={containerRef} className="relative w-full h-full flex-1 flex flex-col">
                {toolbarGrid()}
                <Divider className="m-0" />
                <Table
                    className="base-table w-full h-full flex-1"
                    loading={isLoading}
                    dataSource={data?.result?.items}
                    columns={columns}
                    rowKey={rowKey || ((record) => _.toString(record.id))}
                    pagination={
                        pagination ? { ...paginationState, onChange: handlePageChange } : false
                    }
                    scroll={scrollState}
                    {...restProps}
                />
                {pagination && displayItems()}
            </div>
        </React.Fragment>
    );
};

export default BaseGrid;
