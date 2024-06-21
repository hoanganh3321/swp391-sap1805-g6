import React from "react";

// Admin Imports
import MainDashboard from "./views/admin/default";
import NFTMarketplace from "./views/admin/marketplace";
import DataTables from "./views/admin/tables";
import GoldPrice from "./views/admin/goldprice"

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
  {
    name: "Gold Price",
    layout: "/admin",
    path: "goldprice",
    icon: <MdLock className="w-6 h-6" />,
    component: <GoldPrice />,
  },
];
export default routes;
