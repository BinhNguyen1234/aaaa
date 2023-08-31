import { ReactElement, useMemo } from 'react';

interface sideBarElementProps {
    children: ReactElement | null;
    textContent?: string
}

function SideBarElement({ children, textContent }: sideBarElementProps) {
    const wrappedChildres = useMemo(() => {
        return Array.isArray(children) ? (
            children.map((child,i) => {
                return <li key={i}>{child}</li>;
            })
        ) : (
            <li>{children}</li>
        );
    }, [children]);
    return (
        <>
            <ul>
                {textContent}
                {wrappedChildres}
            </ul>
        </>
    );
}
export default SideBarElement;
