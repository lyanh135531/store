import DrawerContainer from '@/core/layouts/DrawerContainer';
import { BaseFormProps } from '@/types/core';
import { t } from 'i18next';
import React from 'react';
import { Identifier } from 'typescript';

interface Props extends BaseFormProps {
    id?: Identifier;
}

const UserForm: React.FC<Props> = ({ id, ...props }) => {
    return (
        <DrawerContainer title={t('user.edit')} loading {...props}>
            <div>user</div>
        </DrawerContainer>
    );
};

export default UserForm;
