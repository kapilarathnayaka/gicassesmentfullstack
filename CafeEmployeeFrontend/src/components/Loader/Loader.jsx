import { PropagateLoader } from "react-spinners";

const Loader = () => {
  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <PropagateLoader color="#0D1C46" />
    </div>
  );
};

export default Loader;
