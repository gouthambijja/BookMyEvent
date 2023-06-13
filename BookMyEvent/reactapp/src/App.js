import React from "react";
import {
  Route,
  RouterProvider,
  Routes,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import Admin from "./Components/Admin";
import Error from "./Components/Error";
import RequireAuth from "./Components/RequireAuth";
import AdminHomePage from "./Components/AdminHomePage";
import Login from "./Components/AdminLogin";
import Layout from "./Components/Layout";

const App = () => {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route
          path="/"
          element={<Layout />}
          //   action={actions}
          errorElement={<Error />}
        >
          <Route index element={<Login/>}></Route>
          <Route path="/admin" element={<Admin />}>
            <Route index element={<Login />}></Route>
            <Route element={<RequireAuth allowedrole={"Admin"} />}>
              <Route path="/admin/home" element={<AdminHomePage />}></Route>
            </Route>
          </Route>
        </Route>
      </>
    )
  );

  return <RouterProvider router={router} />;
};

export default App;
