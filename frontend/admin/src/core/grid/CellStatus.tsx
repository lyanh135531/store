import { Tag } from 'antd';
import { t } from 'i18next';
import React from 'react';

interface Props {
    value: boolean;
}

const CellStatus: React.FC<Props> = ({ value }) => {
    return (
        <Tag color={value ? 'green' : 'red'} bordered={false}>
            {value ? t('component.active') : t('component.inactive')}
        </Tag>
    );
};

export default CellStatus;
