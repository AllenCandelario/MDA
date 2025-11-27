import { createBrowserRouter, RouterProvider } from "react-router";
import Dashboard from "./routes/Dashboard";



const router = createBrowserRouter([
    { path: "/", element: <Dashboard /> }
]);

export function AppRouter() {
    return <RouterProvider router={router} />;
}