import React from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

import { NavBar } from "./components/NavBar";
import Home from "./pages/Home";
import Devices from "./pages/Devices";
import Profiles from "./pages/Profiles";
import EnergyProfile from "./pages/EnergyProfile";

export function App() {
  return (
    <>
      <Router>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/devices" element={<Devices />} />
          <Route path="/profiles" element={<Profiles />} />
          <Route path="/energyProfile" element={<EnergyProfile />} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
