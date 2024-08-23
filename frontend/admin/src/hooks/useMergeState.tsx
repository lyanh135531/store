import { useState, useCallback } from 'react';

type MergeState<T> = Partial<T> | ((prevState: T) => Partial<T>);

function useMergeState<T>(initialState: T): [T, (newState: MergeState<T>) => void] {
    const [state, setState] = useState<T>(initialState);

    const mergeState = useCallback((newState: MergeState<T>) => {
        setState((prevState) => ({
            ...prevState,
            ...(typeof newState === 'function' ? newState(prevState) : newState)
        }));
    }, []);

    return [state, mergeState];
}

export default useMergeState;
