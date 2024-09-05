import BaseDrawer, { BaseDrawerRef } from '@/core/components/common/BaseDrawer';
import BaseGrid, { BaseGridRef } from '@/core/grid/BaseGrid';
import CellDrawer from '@/core/grid/CellDrawer';
import PageContainer from '@/core/layouts/PageContainer';
import { GRID_API } from '@/pages/user/apis';
import UserForm from '@/pages/user/components/UserForm';
import { UserDto } from '@/pages/user/types/user';
import { ColumnsType } from 'antd/es/table';
import { t } from 'i18next';
import React, { useRef } from 'react';

const UserPage: React.FC = () => {
    const drawerRef = useRef<BaseDrawerRef>(null);
    const gridRef = useRef<BaseGridRef>(null);

    const handleClick = ({ id }: UserDto) => {
        drawerRef.current?.open({
            component: (
                <UserForm
                    id={id}
                    onClose={drawerRef.current?.close}
                    onSuccess={() => {
                        gridRef.current?.reload();
                        drawerRef.current?.close();
                    }}
                />
            )
        });
    };

    const columns: ColumnsType<UserDto> = [
        {
            title: t('user.userName'),
            dataIndex: 'userName',
            render: (value, record, index) => (
                <CellDrawer key={index} value={value} onClick={() => handleClick(record)} />
            )
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
        <PageContainer>
            <BaseGrid
                ref={gridRef}
                gridKey="UserGrid"
                api={{
                    url: GRID_API
                }}
                toolbarConfig={{
                    search: true,
                    create: true
                }}
                columns={columns}
            />
            <BaseDrawer ref={drawerRef} />
        </PageContainer>
    );
};

export default UserPage;
