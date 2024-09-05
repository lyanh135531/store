import React from 'react';

interface Props {
    value: string;
    onClick?: () => void;
}

const CellDrawer: React.FC<Props> = ({ value, onClick }) => {
    return (
        <div className="cursor-pointer hover:underline" onClick={onClick}>
            {value}
        </div>
    );
};

export default CellDrawer;
