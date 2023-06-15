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

import AdminHomePage from "./pages/AdminHomePage";

import Login from "./Components/Login";

import Layout from "./Components/Layout";
import PersistLogin from "./Components/PersistLogin";
import AddSecondary from "./pages/AddSecondary";
import LandingPage from "./pages/LandingPage";

import "./App.css";

import Profile from "./pages/Profile";

import OrganiserHomePage from "./pages/OrganiserHomePage";

import Organiser from "./Components/Organiser";
import AddEvent from "./pages/AddEvent";
import { categoryLoader } from "./Loaders/storeLoader";
import RegisterOrganiser from "./pages/RegisterOrganiser";
import RequestsTable from "./Components/RequestsTable";
import OrganisationsListPage from "./pages/OrganisationsListPage";

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
          <Route index element={<LandingPage />}></Route>

          <Route path="/login" element={<Login />} />
          <Route path="/registerorganiser" element={<RegisterOrganiser/>} />
          <Route path="/orglist" element={<OrganisationsListPage/>} />
          {/* ------------------------------------------------------------------------- */}

          <Route path="organiser" element={<Organiser />}>
            <Route path="login" element={<Login />}></Route>

            <Route element={<PersistLogin />}>
              <Route element={<RequireAuth allowedroles={["Owner", "Peer"]} />}>
                <Route index element={<OrganiserHomePage />}></Route>
                <Route path="AddEvent" element={<AddEvent />}></Route>
                <Route path="addSecondaryOwner" element={<AddSecondary />} />
                <Route path="profile" element={<Profile />} />
              </Route>
            </Route>
          </Route>

          {/* ------------------------------------------------------------------------------- */}

          <Route path="/admin" element={<Admin />}>
            <Route path="login" element={<Login />}></Route>

            <Route element={<PersistLogin />}>
              <Route element={<RequireAuth allowedrole={["Admin"]} />}>
                <Route index element={<AdminHomePage />}></Route>
                <Route path="addadmin" element={<AddSecondary />} />
                <Route path="profile" element={<Profile />} />
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