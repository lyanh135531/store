import BaseDrawer, { BaseDrawerRef } from '@/core/components/common/BaseDrawer';
import { DELETE_FAILED, DELETE_SUCCESS, NOTIFY_TITLE } from '@/core/constants/notify';
import BaseGrid, { BaseGridRef } from '@/core/grid/BaseGrid';
import CellDrawer from '@/core/grid/CellDrawer';
import CellStatus from '@/core/grid/CellStatus';
import PageContainer from '@/core/layouts/PageContainer';
import { DELETE_USER_API, GRID_API } from '@/pages/user/apis';
import UserForm from '@/pages/user/components/UserForm';
import { UserDto } from '@/pages/user/types/user';
import { ApiResponse } from '@/types/auth';
import { ApiUtil } from '@/utils/apiUtil';
import NotifyUtil from '@/utils/notifyUtil';
import { ColumnsType } from 'antd/es/table';
import { t } from 'i18next';
import React, { useRef } from 'react';
import { Identifier } from 'typescript';

const UserPage: React.FC = () => {
    const drawerRef = useRef<BaseDrawerRef>(null);
    const gridRef = useRef<BaseGridRef>(null);

    const onUserNameClick = ({ id }: UserDto) => {
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
            ),
            width: 'medium'
        });
    };

    const onCreate = () => {
        drawerRef.current?.open({
            component: (
                <UserForm
                    onClose={drawerRef.current?.close}
                    onSuccess={() => {
                        gridRef.current?.reload();
                        drawerRef.current?.close();
                    }}
                />
            ),
            width: 'medium'
        });
    };

    const onDelete = async (id?: Identifier) => {
        if (id) {
            gridRef.current?.mask();
            await ApiUtil.Axios<ApiResponse<UserDto>>('delete', DELETE_USER_API, {
                id
            })
                .then((res) => {
                    if (res?.data?.success) {
                        NotifyUtil.success(NOTIFY_TITLE, DELETE_SUCCESS);
                        gridRef.current?.reload();
                    } else {
                        NotifyUtil.error(NOTIFY_TITLE, DELETE_FAILED);
                    }
                })
                .finally(() => gridRef.current?.unmask());
        }
    };

    const columns: ColumnsType<UserDto> = [
        {
            title: t('user.userName'),
            dataIndex: 'userName',
            render: (value, record, index) => (
                <CellDrawer key={index} value={value} onClick={() => onUserNameClick(record)} />
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
            title: t('user.phone'),
            dataIndex: 'phoneNumber',
            width: 150
        },
        {
            title: t('user.status'),
            dataIndex: 'status',
            width: 120,
            render: (value) => <CellStatus value={value} />
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
                    create: true,
                    onCreate: onCreate
                }}
                columns={columns}
                actionRow={{
                    items: [
                        {
                            type: 'detail',
                            onClick: (record) => {
                                console.log('View detail of', record);
                            }
                        },
                        {
                            type: 'edit',
                            onClick: (record) => {
                                console.log('Edit', record);
                            }
                        },
                        {
                            type: 'delete',
                            onClick: (record) => onDelete(record?.id)
                        }
                    ]
                }}
            />
            <BaseDrawer ref={drawerRef} />
        </PageContainer>
    );
};

export default UserPage;
