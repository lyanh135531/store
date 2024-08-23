import React from 'react';
import { Spin } from 'antd';
import { SpinProps } from 'antd/lib/spin';

interface LoadingProps extends SpinProps {
    tip?: string;
    size?: 'small' | 'default' | 'large';
    fullScreen?: boolean;
}

const Loading: React.FC<LoadingProps> = ({
    tip = 'Loading',
    size = 'default',
    fullScreen = false,
    ...rest
}) => {
    const contentStyle: React.CSSProperties = {
        padding: 50,
        background: 'rgba(0, 0, 0, 0.05)',
        borderRadius: 4
    };

    const content = <div style={contentStyle} />;

    if (fullScreen) {
        return (
            <div
                style={{
                    position: 'fixed',
                    top: 0,
                    left: 0,
                    width: '100%',
                    height: '100%',
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    backgroundColor: 'rgba(255, 255, 255, 0.7)',
                    zIndex: 1000
                }}
            >
                <Spin size={size} tip={tip} {...rest}>
                    {content}
                </Spin>
            </div>
        );
    }

    return (
        <div className="flex justify-center w-full h-full items-center">
            <Spin size={size} tip={tip} {...rest}>
                {content}
            </Spin>
        </div>
    );
};

export default Loading;
