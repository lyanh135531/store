import { BreadcrumbItem } from '@/hooks/useBreadcrumb';
import { Breadcrumb } from 'antd';
import _ from 'lodash';
import React from 'react';

interface Props {
  menuItems: BreadcrumbItem[];
}

const BaseBreadcrumb: React.FC<Props> = ({ menuItems }) => {
  const renderBreadcrumb = (item: BreadcrumbItem) => {
    if (item.href && !_.lastIndexOf(menuItems, item)) {
      return <a href={item.href}>{item.title}</a>;
    }
    return item.title;
  };

  return (
    <Breadcrumb>
      {menuItems.map((item, index) => (
        <Breadcrumb.Item key={index}>{renderBreadcrumb(item)}</Breadcrumb.Item>
      ))}
    </Breadcrumb>
  );
};

export default BaseBreadcrumb;
