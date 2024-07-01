import React from "react";

// Admin Imports
import MainDashboard from "./views/admin/default";
import NFTMarketplace from "./views/admin/marketplace";
import DataTables from "./views/admin/tables";
import GoldPrice from "./views/admin/goldprice"
import ReturnPolicyTable from "./views/admin/returnpolicy";
import PromotionTable from "./views/admin/promotion";
import StaffTable from "./views/admin/staff";
import StoreTable from "./views/admin/store";
// // Auth Imports
// import SignIn from "views/auth/SignIn";

// Icon Imports
import {
  MdHome,
  MdOutlineShoppingCart,
  MdBarChart,
  MdPerson,
  MdLock,
  MdPolicy,
  MdCardGiftcard,
  MdPeople,
  MdStore
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
    name: "Gold Price Display",
    layout: "/admin",
    path: "goldprice",
    icon: <MdLock className="w-6 h-6" />,
    component: <GoldPrice />,
  },
  {
    name: "Return Policy",
    layout: "/admin",
    path: "returnpolicy",
    icon: <MdPolicy className="w-6 h-6" />,
    component: <ReturnPolicyTable />,
  },
  {
    name: "Promotions",
    layout: "/admin",
    path: "promotion",
    icon: <MdCardGiftcard className="w-6 h-6" />,
    component: <PromotionTable />,
  },
  {
    name: "Staff",
    layout: "/admin",
    path: "staff",
    icon: <MdPeople className="w-6 h-6" />,
    component: <StaffTable />,
  },
  {
    name: "Store",
    layout: "/admin",
    path: "store",
    icon: <MdStore className="w-6 h-6" />,
    component: <StoreTable />,
  }
];
export default routes;
