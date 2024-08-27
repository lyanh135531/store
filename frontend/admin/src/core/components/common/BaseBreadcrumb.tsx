import { BreadcrumbItem } from '@/hooks/useBreadcrumb';
import { Breadcrumb } from 'antd';
import React from 'react';
import { Link } from 'react-router-dom';

interface Props {
    menuItems: BreadcrumbItem[];
}

const BaseBreadcrumb: React.FC<Props> = ({ menuItems }) => {
    const breadcrumbItems = menuItems.map((item, index) => {
        const isLastItem = index === menuItems.length - 1;
        return {
            key: index,
            title: item.href && !isLastItem ? <Link to={item.href}>{item.title}</Link> : item.title
        };
    });

    return <Breadcrumb items={breadcrumbItems} />;
};

export default BaseBreadcrumb;
