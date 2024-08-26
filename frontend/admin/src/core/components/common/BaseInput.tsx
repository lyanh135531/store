import React from 'react';
import { Input, InputProps } from 'antd';

type BaseInputProps = InputProps;

const BaseInput: React.FC<BaseInputProps> = (props) => {
    return <Input {...props} />;
};

export default BaseInput;
