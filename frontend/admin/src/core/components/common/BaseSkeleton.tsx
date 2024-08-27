import { Col, Row, Skeleton } from 'antd';
import React from 'react';

const SkeletonLoader: React.FC = () => (
    <div className="px-6">
        <Row gutter={16}>
            <Col span={24}>
                <Skeleton active paragraph={{ rows: 4 }} />
            </Col>
        </Row>
    </div>
);

export default SkeletonLoader;
