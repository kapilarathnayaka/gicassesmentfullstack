import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const employeeApi = createApi({
  reducerPath: 'employeeApi',
  baseQuery: fetchBaseQuery({ 
    baseUrl: 'http://localhost:5232/api/'
  }),
  endpoints: (builder) => ({
    getEmployees: builder.query({
      query: () => `Employee`,
    }),
    getEmployeesByCafeId: builder.query({
      query: (id) => `Employee/by-cafe/${id}`,
    }),
    getEmployeeById: builder.query({
      query: (id) => `Employee/${id}`,
    }),
    createEmployee: builder.mutation({
      query: (newEmployee) => ({
        url: 'Employee',
        method: 'POST',
        body: newEmployee,
      }),
    }),
    updateEmployee: builder.mutation({
      query: (updatedEmployee) => ({
        url: `Employee/${updatedEmployee.id}`,
        method: 'PUT',
        body: updatedEmployee,
      }),
    }),
    addEmployee: builder.mutation({
      query: (newEmployee) => ({
        url: 'Employee',
        method: 'POST',
        body: newEmployee,
      }),
    }),
    deleteEmployee: builder.mutation({
      query: (id) => ({
        url: `Employee/${id}`,
        method: 'DELETE',
      }),
    }),
  }),
});

export const { 
  useGetEmployeesQuery, 
  useAddEmployeeMutation, 
  useDeleteEmployeeMutation, 
  useCreateEmployeeMutation, 
  useUpdateEmployeeMutation,
  useGetEmployeesByCafeIdQuery,
  useGetEmployeeByIdQuery
} = employeeApi;