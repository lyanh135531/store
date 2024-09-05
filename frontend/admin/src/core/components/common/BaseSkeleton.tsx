import { Col, Row, Skeleton } from 'antd';
import clsx from 'clsx';
import React from 'react';

interface Props {
    className?: string;
}

const SkeletonLoader: React.FC<Props> = ({ className }) => (
    <div className={clsx('p-6', className)}>
        <Row gutter={16}>
            <Col span={24}>
                <Skeleton active paragraph={{ rows: 4 }} />
            </Col>
        </Row>
    </div>
);

export default SkeletonLoader;
