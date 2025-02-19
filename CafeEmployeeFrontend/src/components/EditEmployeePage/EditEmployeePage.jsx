import { useParams } from "react-router-dom";
import { useGetEmployeeByIdQuery } from "../../api/employeeApi";
import EmployeeForm from "../EmployeeForm/EmployeeForm";

const EditEmployeePage = () => {
  const { id } = useParams(); // Get cafeId from URL
  const { data: employeeData, isLoading } = useGetEmployeeByIdQuery(id);

  if (isLoading) return <p>Loading...</p>;
  if (!employeeData) return <p>No Employee found</p>;

  return <EmployeeForm initialData={employeeData} />;
};

export default EditEmployeePage;
