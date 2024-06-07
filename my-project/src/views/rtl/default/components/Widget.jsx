import Card from "../../../../components/card";

const Widget = ({ icon, title, subtitle }) => {
  return (
    <Card extra="!flex-row flex-grow items-center rounded-[20px]">
      <div className="mr-4 flex h-[90px] w-auto flex-row items-center">
        <div className="p-3 rounded-full bg-lightPrimary dark:bg-navy-700">
          <span className="flex items-center text-brand-500 dark:text-white">
            {icon}
          </span>
        </div>
      </div>

      <div className="flex flex-col justify-center w-auto mr-4 h-50">
        <p className="text-sm font-medium text-gray-600 font-dm">{title}</p>
        <h4 className="text-xl font-bold text-navy-700 dark:text-white">
          {subtitle}
        </h4>
      </div>
    </Card>
  );
};

export default Widget;
