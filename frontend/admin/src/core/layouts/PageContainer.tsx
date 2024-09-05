import React, { PropsWithChildren } from 'react';

type Props = PropsWithChildren;

const PageContainer: React.FC<Props> = ({ children }) => {
    return <div className="w-full h-full">{children}</div>;
};

export default PageContainer;
