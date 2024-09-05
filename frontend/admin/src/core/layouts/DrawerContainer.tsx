import BaseButton from '@/core/components/buttons/BaseButton';
import SkeletonLoader from '@/core/components/common/BaseSkeleton';
import { BaseFormProps } from '@/types/core';
import { Space } from 'antd';
import { t } from 'i18next';
import React, { PropsWithChildren } from 'react';

interface Props extends PropsWithChildren, BaseFormProps {
    title: string;
    loading?: boolean;
}

const DrawerContainer: React.FC<Props> = ({ title, loading, children, onClose, onSuccess }) => {
    return (
        <div className="flex flex-col w-full h-full">
            <div className="flex justify-between ant-drawer-header">
                <span className="ant-drawer-title">{title}</span>
                <Space>
                    <BaseButton onClick={onClose}>{t('button.cancel')}</BaseButton>
                    <BaseButton variants="primary" onClick={onSuccess}>
                        {t('button.save')}
                    </BaseButton>
                </Space>
            </div>
            {loading ? <SkeletonLoader /> : <div className="flex-1 p-4">{children}</div>}
        </div>
    );
};

export default DrawerContainer;
