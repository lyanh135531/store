import React from 'react';

interface Props {
    value: string;
    onClick?: () => void;
}

const CellDrawer: React.FC<Props> = ({ value, onClick }) => {
    return (
        <div>
            <span className="cursor-pointer hover:text-main-color-primary" onClick={onClick}>
                {value}
            </span>
        </div>
    );
};

export default CellDrawer;
