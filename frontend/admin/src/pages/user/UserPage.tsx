import BaseGrid from '@/core/grid/BaseGrid';
import { GRID_API } from '@/pages/user/apis';
import { UserDto } from '@/pages/user/types/user';
import { ColumnsType } from 'antd/es/table';
import { t } from 'i18next';
import React from 'react';

const UserPage: React.FC = () => {
    const columns: ColumnsType<UserDto> = [
        {
            title: t('user.userName'),
            dataIndex: 'userName'
        },
        {
            title: t('user.fullName'),
            dataIndex: 'fullName'
        },
        {
            title: t('user.email'),
            dataIndex: 'email'
        },
        {
            title: t('user.gender'),
            dataIndex: 'gender',
            width: 100
        },
        {
            title: t('user.phone'),
            dataIndex: 'phoneNumber',
            width: 150
        }
    ];

    return (
        <BaseGrid
            gridKey="UserGrid"
            api={{
                url: GRID_API
            }}
            toolbarConfig={{
                search: true
            }}
            columns={columns}
        />
    );
};

export default UserPage;
