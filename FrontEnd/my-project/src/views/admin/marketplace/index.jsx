import { useEffect, useState } from "react";
import tableDataTopCreators from "./variables/tableDataTopCreators.json";
import { tableColumnsTopCreators } from "./variables/tableColumnsTopCreators";
import HistoryCard from "./components/HistoryCard";
import TopCreatorTable from "./components/TableTopCreators";
import NftCard from "../../../components/card/NftCard";
import axios from "axios";

const Marketplace = () => {
  const [ListProduct, setListProduct] = useState([]);
  
  useEffect(() => {
    axios
      .get("https://localhost:7002/api/product/list")
      .then((response) => {
        setListProduct(response.data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);
  return (
    <div className="grid w-full h-full grid-cols-1 gap-5 mt-3 xl:grid-cols-1 2xl:grid-cols-1">
  <div className="w-full col-span-1 h-fit xl:col-span-1 2xl:col-span-1">
    <div className="flex flex-col justify-between px-4 mt-5 mb-4 md:flex-row md:items-center">
      <h4 className="ml-1 text-2xl font-bold text-navy-700 dark:text-white">
        All products
      </h4>
    </div>

    {/* NFTs trending card */}
    <div className="z-20 grid grid-cols-1 gap-5 md:grid-cols-3">
      {ListProduct.map((list, index) => (
        <NftCard
          key={index}
          index={index}
          name={list?.productName}
          price={list?.price}
          image={list?.image}
        />
      ))}
    </div>
  </div>
</div>

  );
};

export default Marketplace;
