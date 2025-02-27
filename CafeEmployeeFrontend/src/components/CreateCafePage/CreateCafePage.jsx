import CafeForm from '../CafeForm/CafeForm';
const CreateCafePage = () => {
  return (
    <div>
      <CafeForm /> {/* Reuse the form but no need for initialData */}
    </div>
  );
};

export default CreateCafePage;
