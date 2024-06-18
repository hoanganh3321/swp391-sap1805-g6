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
    <div className="grid h-full grid-cols-1 gap-5 mt-3 xl:grid-cols-2 2xl:grid-cols-3">
      <div className="w-full col-span-1 h-fit xl:col-span-1 2xl:col-span-2">
        {/* NFt Banner */}
        {/* <Banner /> */}

        {/* NFt Header */}
        <div className="flex flex-col justify-between px-4 mt-5 mb-4 md:flex-row md:items-center">
          <h4 className="ml-1 text-2xl font-bold text-navy-700 dark:text-white">
            All products
          </h4>
          <ul className="mt-4 flex items-center justify-between md:mt-0 md:justify-center md:!gap-5 2xl:!gap-12">
            <li>
              <a
                className="text-base font-medium text-brand-500 hover:text-brand-500 dark:text-white"
                href=" "
              >
                Art
              </a>
            </li>
            <li>
              <a
                className="text-base font-medium text-brand-500 hover:text-brand-500 dark:text-white"
                href=" "
              >
                Music
              </a>
            </li>
            <li>
              <a
                className="text-base font-medium text-brand-500 hover:text-brand-500 dark:text-white"
                href=" "
              >
                Collection
              </a>
            </li>
            <li>
              <a
                className="text-base font-medium text-brand-500 hover:text-brand-500 dark:text-white"
                href=" "
              >
                <a href=" ">Sports</a>
              </a>
            </li>
          </ul>
        </div>

        {/* NFTs trending card */}
        <div className="z-20 grid grid-cols-1 gap-5 md:grid-cols-3">
          {ListProduct.map((list, index) => (
            <NftCard
              index={index}
              name={list?.productName}
              categoryID={list?.categoryId}
              price={list?.price}
              image={list?.image}
            />
          ))}
        </div>
      </div>
      <div className="w-full h-full col-span-1 rounded-xl 2xl:col-span-1">
        <TopCreatorTable
          extra="mb-5"
          tableData={tableDataTopCreators}
          columnsData={tableColumnsTopCreators}
        />
        <HistoryCard />
      </div>
    </div>
  );
};

export default Marketplace;
