import BaseGrid from '@/core/grid/BaseGrid';
import { GRID_API } from '@/pages/user/apis';
import { UserDto } from '@/pages/user/types/user';
import { ColumnsType } from 'antd/es/table';
import React from 'react';

const UserPage: React.FC = () => {
    const columns: ColumnsType<UserDto> = [
        {
            title: 'User Name',
            dataIndex: 'userName'
        },
        {
            title: 'Full Name',
            dataIndex: 'fullName'
        },
        {
            title: 'Email',
            dataIndex: 'email'
        },
        {
            title: 'Gender',
            dataIndex: 'gender',
            width: 100
        },
        {
            title: 'Phone',
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
