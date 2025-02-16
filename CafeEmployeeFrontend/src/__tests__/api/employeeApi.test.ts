import { setupServer } from 'msw/node';
import { rest } from 'msw';
import { renderHook } from '@testing-library/react-hooks';
import { Provider } from 'react-redux';
import { store } from '../../store';
import { useGetEmployeesQuery, useAddEmployeeMutation, useDeleteEmployeeMutation } from '../../api/employeeApi';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const server = setupServer(
  rest.get('http://localhost:5000/api/employees', (req, res, ctx) => {
    return res(ctx.json([{ id: '1', name: 'Employee 1', email_address: 'employee1@example.com', phone_number: '1234567890', days_worked: 5, cafe: 'Cafe 1' }]));
  }),
  rest.post('http://localhost:5000/api/employee', (req, res, ctx) => {
    return res(ctx.status(201));
  }),
  rest.delete('http://localhost:5000/api/employee/:id', (req, res, ctx) => {
    return res(ctx.status(204));
  })
);

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());
afterAll(() => server.close());

const queryClient = new QueryClient();

const wrapper = ({ children }) => (
  <Provider store={store}>
    <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
  </Provider>
);

describe('employeeApi', () => {
  test('useGetEmployeesQuery', async () => {
    const { result, waitFor } = renderHook(() => useGetEmployeesQuery(), { wrapper });

    await waitFor(() => result.current.isSuccess);

    expect(result.current.data).toEqual([{ id: '1', name: 'Employee 1', email_address: 'employee1@example.com', phone_number: '1234567890', days_worked: 5, cafe: 'Cafe 1' }]);
  });

  test('useAddEmployeeMutation', async () => {
    const { result, waitFor } = renderHook(() => useAddEmployeeMutation(), { wrapper });

    result.current[0]({ name: 'New Employee', email_address: 'newemployee@example.com', phone_number: '0987654321', days_worked: 3, cafe: 'Cafe 2' });

    await waitFor(() => result.current[1].isSuccess);

    expect(result.current[1].isSuccess).toBe(true);
  });

  test('useDeleteEmployeeMutation', async () => {
    const { result, waitFor } = renderHook(() => useDeleteEmployeeMutation(), { wrapper });

    result.current[0]('1');

    await waitFor(() => result.current[1].isSuccess);

    expect(result.current[1].isSuccess).toBe(true);
  });
});
