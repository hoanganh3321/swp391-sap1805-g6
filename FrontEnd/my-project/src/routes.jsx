import React from "react";

// Admin Imports
import MainDashboard from "./views/admin/default";
import NFTMarketplace from "./views/admin/marketplace";
import Profile from "./views/admin/profile";
import DataTables from "./views/admin/tables";

// // Auth Imports
// import SignIn from "views/auth/SignIn";

// Icon Imports
import {
  MdHome,
  MdOutlineShoppingCart,
  MdBarChart,
  MdPerson,
  MdLock,
} from "react-icons/md";

const routes = [
  {
    name: "Main Dashboard",
    layout: "/admin",
    path: "default",
    icon: <MdHome className="w-6 h-6" />,
    component: <MainDashboard />,
  },
  {
    name: "Product",
    layout: "/admin",
    path: "nft-marketplace",
    icon: <MdOutlineShoppingCart className="w-6 h-6" />,
    component: <NFTMarketplace />,
    secondary: true,
  },
  {
    name: "Data Tables",
    layout: "/admin",
    icon: <MdBarChart className="w-6 h-6" />,
    path: "data-tables/",
    component: <DataTables />,
  },
  // {
  //   show : "hidden",
  //   name: "create material",
  //   layout: "/admin",
  //   path: "data-tables/material/create",
  //   component: <Create label={"Material"}/>,
  // },
  // {
  //   show : "hidden",
  //   name: "create Type",
  //   layout: "/admin",
  //   path: "data-tables/type/create",
  //   component: <Create label={"Type"}/>,
  // },
  // {
  //   show : "hidden",
  //   name: "create gemstone",
  //   layout: "/admin",
  //   path: "data-tables/gemstone/create",
  //   component: <Create label={"Gemstone"}/>,
  // },
  {
    name: "Profile",
    layout: "/admin",
    path: "profile",
    icon: <MdPerson className="w-6 h-6" />,
    component: <Profile />,
  },
];
export default routes;
