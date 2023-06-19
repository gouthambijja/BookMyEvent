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
import storeLoader from "./Loaders/storeLoader";
import RegisterOrganiser from "./pages/RegisterOrganiser";
import OrganisationsListPage from "./pages/OrganisationsListPage";
import PeerRequest from "./pages/PeerRequests";
import OrganisationTree from "./pages/OrganisationTree";
// import EventDynamicForm from "./Components/EventDynamicForm";
import MediaCard from "./Components/Events";

const App = () => {
    const router = createBrowserRouter(
        createRoutesFromElements(
            <>
                <Route
                    path="/"
                    element={<Layout />}
                    //   action={actions}
                    errorElement={<Error />}
                    loader={storeLoader.categoryLoader}
                >
                    <Route index element={<LandingPage />}></Route>
                    <Route path="/login" element={<Login />} />
                    <Route path="/orglist" element={<OrganisationsListPage />} />
                    {/* ------------------------------------------------------------------------- */}

                    <Route path="organiser" element={<Organiser />}>
                        <Route path="login" element={<Login />}></Route>
                        <Route path="register" element={<RegisterOrganiser />} />
                        <Route element={<PersistLogin />}>
                            <Route element={<RequireAuth allowedroles={["Owner", "Peer", "Secondary_Owner"]} />}>
                                <Route index element={<OrganiserHomePage />}></Route>
                                {/* <Route path="createNewEventRegistrationForm" element={<EventDynamicForm/>} loader={storeLoader.FormFieldsLoader}/> */}
                                <Route path="AddEvent" element={<AddEvent />} loader={storeLoader.OrganisationFormLoaders} ></Route>
                                <Route path="OrganisationTree" element={<OrganisationTree />} loader={storeLoader.OrganisationTreeLoader}></Route>
                                <Route path="PeerRequests" element={<PeerRequest />}></Route>
                                <Route path="addSecondaryOwner" element={<AddSecondary />} />
                                <Route path="profile" element={<Profile />} />
                            </Route>
                        </Route>
                    </Route>

                    {/* ------------------------------------------------------------------------------- */}

                    <Route path="/admin" element={<Admin />}>
                        <Route path="login" element={<Login />}></Route>
                        <Route element={<PersistLogin />}>
                            <Route element={<RequireAuth allowedroles={["Admin"]} />}>
                                <Route index element={<AdminHomePage />}></Route>
                                <Route path="addadmin" element={<AddSecondary />} />
                                <Route path="Organisations" element={<OrganisationsListPage />} loader={storeLoader.OrganisationsLoader} />
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