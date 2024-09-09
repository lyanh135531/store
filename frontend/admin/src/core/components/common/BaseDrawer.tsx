import useMergeState from '@/hooks/useMergeState';
import { Drawer } from 'antd';
import React, { ReactNode, useImperativeHandle } from 'react';

export interface BaseDrawerProps {
    className?: string;
}

type WidthDrawer = 'default' | 'medium' | 'large';

export interface BaseDrawerRef {
    open: (params: { component: ReactNode; width?: WidthDrawer }) => void;
    close: () => void;
}

type State = {
    open: boolean;
    component: ReactNode;
    width?: WidthDrawer;
};

const widthMapping = {
    default: '30%',
    medium: '40%',
    large: '50%'
};

const BaseDrawer = React.forwardRef<BaseDrawerRef, BaseDrawerProps>((props, ref) => {
    const [state, setState] = useMergeState<State>({
        open: false,
        component: null,
        width: 'default'
    });

    useImperativeHandle(ref, () => ({
        open({ component, width = 'default' }) {
            setState({ component, width, open: true });
        },
        close() {
            setState({ open: false });
        }
    }));

    return (
        <Drawer
            placement="right"
            closable={false}
            destroyOnClose
            maskClosable={false}
            onClose={() => setState({ open: false })}
            open={state.open}
            width={widthMapping[state.width || 'default']}
            {...props}
        >
            {state.component}
        </Drawer>
    );
});

BaseDrawer.displayName = 'BaseDrawer';

export default BaseDrawer;
