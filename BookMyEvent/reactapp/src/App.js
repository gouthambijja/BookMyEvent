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
import PersistLogin from "./Components/PersistLogin";

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
          <Route index element={<Login />}></Route>

          <Route path="/admin" element={<Admin />}>
            <Route path="login" element={<Login />}></Route>
            <Route element={<PersistLogin />}>
              <Route element={<RequireAuth allowedrole={"Admin"} />}>
                <Route index  element={<AdminHomePage />}></Route>
              </Route>
            </Route>
          </Route>
        </Route>
      </>
    )
  );

  return <RouterProvider router={router} />;
};

export default App;
