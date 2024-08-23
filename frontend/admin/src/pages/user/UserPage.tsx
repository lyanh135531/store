import BaseGrid from '@/core/grid/BaseGrid';
import { Entity } from '@/types/core';
import { ColumnsType } from 'antd/es/table';
import React, { useEffect, useState } from 'react';

interface DataType extends Entity {
    name: string;
    age: number;
    address: string;
}

const UserPage: React.FC = () => {
    const [data, setData] = useState<DataType[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        // Giả lập lấy dữ liệu
        setTimeout(() => {
            setData([
                { id: '1', name: 'John Doe', age: 32, address: 'New York' },
                { id: '2', name: 'Jane Doe', age: 28, address: 'London' },
                { id: '3', name: 'Jane Doe', age: 28, address: 'asd' },
                { id: '4', name: 'Jane Doe', age: 28, address: 'Lonasddon' },
                { id: '5', name: 'Jane Doe', age: 28, address: 'Lonasddon' },
                { id: '6', name: 'Jane Doe', age: 28, address: 'Longgfdfhdfdon' },
                { id: '7', name: 'Jane Doe', age: 28, address: 'Loasdndon' },
                { id: '8', name: 'Jane Doe', age: 28, address: 'Lonjgfjfdon' },
                { id: '9', name: 'Jane Doe', age: 28, address: 'Longjghdon' },
                { id: '10', name: 'Jane Doe', age: 28, address: 'Lokgjlhndon' },
                { id: '11', name: 'Jane Doe', age: 28, address: 'Lojljndon' },
                { id: '12', name: 'Jane Doe', age: 28, address: 'Lojl;k;ndon' },
                { id: '13', name: 'Jane Doe', age: 28, address: 'Lokllkndon' },
                { id: '14', name: 'Jane Doe', age: 28, address: 'Lol;;ndon' },
                { id: '15', name: 'Jane Doe', age: 28, address: 'Lonl;don' }
            ]);
            setLoading(false);
        }, 1000);
    }, []);

    const columns: ColumnsType<DataType> = [
        {
            title: 'Name',
            dataIndex: 'name',
            filters: [
                { text: 'Male', value: 'male' },
                { text: 'Female', value: 'female' }
            ],
            key: 'name'
        },
        {
            title: 'Age',
            dataIndex: 'age',
            key: 'age'
        },
        {
            title: 'Address',
            dataIndex: 'address',
            key: 'address'
        }
    ];

    return <BaseGrid loading={loading} dataSource={data} columns={columns} />;
};

export default UserPage;
