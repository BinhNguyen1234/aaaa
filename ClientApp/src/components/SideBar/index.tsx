import { useMemo, ReactElement } from 'react';
interface sideBarProps {
    children: ReactElement[] | ReactElement;
}

function SideBar({ children }: sideBarProps) {
    const wrappedChildrens = useMemo(() => {
        return Array.isArray(children) ? (
            children.map((child) => {
                return <li>{child}</li>;
            })
        ) : (
            <li>{children}</li>
        );
    }, [children]);
    return (
        <>
            <ul>{wrappedChildrens}</ul>
        </>
    );
}

export default SideBar;
