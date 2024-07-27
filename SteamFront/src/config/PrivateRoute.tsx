import React from 'react';
import { Navigate } from 'react-router-dom';

interface PrivateRouteProps {
    children: React.ReactNode;
    url: string;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children , url}) => {
    const isAuthenticated = !!localStorage.getItem('token'); // Замініть цю перевірку на свою
    return isAuthenticated ? children : <Navigate to={url} />;
};

export default PrivateRoute;
