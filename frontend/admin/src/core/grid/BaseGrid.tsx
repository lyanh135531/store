import useMergeState from '@/hooks/useMergeState';
import { Entity } from '@/types/core';
import { Table, TableProps } from 'antd';
import _ from 'lodash';
import React, { useCallback, useEffect, useRef } from 'react';

interface BaseGridProps<T> extends TableProps<T> {
    loading?: boolean;
    dataSource: T[];
    columns: TableProps<T>['columns'];
    rowKey?: string | ((record: T) => string);
    pageSize?: number;
}

const BaseGrid = <T extends Entity>({
    loading,
    dataSource,
    columns,
    rowKey,
    pageSize = 15,
    ...restProps
}: BaseGridProps<T>) => {
    const containerRef = useRef<HTMLDivElement>(null);
    const [scrollState, setScrollState] = useMergeState<TableProps<T>['scroll']>({});

    const calculateScroll = useCallback(() => {
        if (containerRef.current) {
            const container = containerRef.current.querySelector(
                '.ant-table-wrapper'
            ) as HTMLDivElement;
            const tbody = containerRef.current.querySelector('.ant-table-tbody') as HTMLDivElement;
            const pagination = containerRef.current.querySelector(
                '.ant-table-pagination'
            ) as HTMLDivElement;
            const tHeader = containerRef.current.querySelector(
                '.ant-table-thead'
            ) as HTMLDivElement;

            const containerHeight = container?.clientHeight || 0;
            const tbodyHeight = tbody?.scrollHeight || 0;
            const paginationHeight = pagination?.offsetHeight || 0;
            const tHeaderHeight = tHeader?.clientHeight || 0;
            const topBottomHeight = paginationHeight * 2 + tHeaderHeight;

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
    }, [calculateScroll, dataSource]);

    return (
        <React.Fragment>
            <div ref={containerRef} className="w-full h-full flex-1">
                <Table
                    className="base-table w-full h-full flex-1"
                    loading={loading}
                    dataSource={dataSource}
                    columns={columns}
                    rowKey={rowKey || ((record) => _.toString(record.id))}
                    pagination={{ pageSize }}
                    scroll={scrollState}
                    {...restProps}
                />
            </div>
        </React.Fragment>
    );
};

export default BaseGrid;
