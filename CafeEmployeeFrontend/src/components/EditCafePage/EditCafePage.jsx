import { useParams } from "react-router-dom";
import { useGetCafeByIdQuery } from "../../api/cafeApi";
import CafeForm from "../CafeForm/CafeForm";

const EditCafePage = () => {
  const { id } = useParams(); // Get cafeId from URL
  const { data: cafeData, isLoading } = useGetCafeByIdQuery(id);
  if (isLoading) return <p>Loading...</p>;
  if (!cafeData) return <p>No cafe found</p>;

  return <CafeForm initialCafeData={cafeData} />;
};

export default EditCafePage;
