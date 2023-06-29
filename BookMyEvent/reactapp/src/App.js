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
import EventDynamicForm from "./Components/EventDynamicForm";
import OrganisationTree from "./pages/OrganisationTree";
import OrganiserEventPage from "./pages/OrganiserEventPage";
// import EventDynamicForm from "./Components/EventDynamicForm";
import Event from "./Components/Event";
import RegisterUser from "./pages/RegisterUser";
import OwnerRequests from "./pages/OwnerRequests";
import RegisterEvent from "./Components/RegisterEvent";
import store from "./App/store";
import ProfilePage from "./pages/Profile";
import InfiniteMyPastEvents from "./Components/InfiniteMyPastEvents";
import InfiniteOrganisationPastEvents from "./Components/InfiniteOrganisationPastEvents";
import EventRequestsPage from "./pages/EventRequestsPage";
import OrganisationEventsPage from "./pages/OrganisationEventsPage";

import "react-toastify/dist/ReactToastify.css";
import UserTicketList from "./pages/UserTickets";
import SingleEventPage from "./Components/SingleEventPage";
import RegisteredEvents from "./pages/RegisteredEvents";
import EventTickets from "./pages/EventTickets";
import EditEventCard from "./Components/EditEventCard";



const App = () => {
  const profile = store.getState().profile.info;

  const router = createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route
          path="/"
          element={<Layout />}
          //action={actions}
          errorElement={<Error />}
          loader={storeLoader.categoryLoader}
        >
          <Route element={<PersistLogin />}>
          <Route
            index
            element={<LandingPage />}
            loader={storeLoader.LandingPageEventsLoader}
          ></Route>
          <Route path="/event/:id" element={<SingleEventPage />} />
          <Route path="/login" element={<Login />} />
          <Route path="/Register" element={<RegisterUser />} />
            <Route element={<RequireAuth allowedroles={["User"]} />}>
              <Route path="/tickets/:eventId" element={<UserTicketList />} />
              <Route path="/registeredEvents" element={<RegisteredEvents/>}/>
              <Route
                path="/registerEvent/:eventId/:formId"
                element={<RegisterEvent />}
                loader={storeLoader.FormFieldsLoader}
              ></Route>
              <Route path="profile" element={<Profile />} />
            </Route>
          </Route>
          {/* ------------------------------------------------------------------------- */}
          <Route path="organiser" element={<Organiser />}>
            <Route path="login" element={<Login />}></Route>
            <Route path="register" element={<RegisterOrganiser />} />
            <Route element={<PersistLogin />}>
              <Route
                element={
                  <RequireAuth
                    allowedroles={["Owner", "Peer", "Secondary_Owner"]}
                  />
                }
              >
                <Route index element={<OrganiserHomePage />}></Route>
                <Route
                  path="createNewEventRegistrationForm"
                  element={<EventDynamicForm />}
                  loader={storeLoader.FormFieldsLoader}
                />
                <Route
                  path="AddEvent"
                  element={<AddEvent />}
                  loader={storeLoader.OrganisationFormLoaders}
                ></Route>
                <Route
                  path="OrganisationTree/:id"
                  element={<OrganisationTree />}
                ></Route>
                <Route path="PeerRequests" element={<PeerRequest />}></Route>
                <Route path="addSecondaryOwner" element={<AddSecondary />} />
                <Route path="profile" element={<Profile />} />
                <Route path="eventReq" element={<EventRequestsPage />} />
                <Route path="myPastEvents" element={<InfiniteMyPastEvents />} />
                <Route
                  path="organisationEvents"
                  element={<OrganisationEventsPage />}
                />
                <Route path="EditEvent/:id" element={<EditEventCard />} />
                <Route
                  path="organisationPastEvents"
                  element={<InfiniteOrganisationPastEvents />}
                />
                <Route path="event/:eventId" element={<EventTickets/>}/>
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
                <Route
                  path="Organisations"
                  element={<OrganisationsListPage />}
                  loader={storeLoader.OrganisationsLoader}
                >
                  {" "}
                </Route>
                <Route
                  path="Organisations/:id"
                  element={<OrganisationTree />}
                />
                <Route path="profile" element={<Profile />} />
              </Route>
            </Route>
          </Route>
        </Route>
      </>
    )
  );

  return <RouterProvider router={router}></RouterProvider>;
};

export default App;
