import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { Employee } from '../types/employee';

export const employeeApi = createApi({
  reducerPath: 'employeeApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5000/api/' }),
  endpoints: (builder) => ({
    getEmployees: builder.query<Employee[], void>({
      query: () => `employees`,
    }),
    addEmployee: builder.mutation<void, Partial<Employee>>({
      query: (newEmployee) => ({
        url: 'employee',
        method: 'POST',
        body: newEmployee,
      }),
    }),
    deleteEmployee: builder.mutation<void, string>({
        query: (id) => ({
          url: `employee/${id}`,
          method: 'DELETE',
        }),
      }),
  }),
});

export const { useGetEmployeesQuery, useAddEmployeeMutation,useDeleteEmployeeMutation } = employeeApi;
