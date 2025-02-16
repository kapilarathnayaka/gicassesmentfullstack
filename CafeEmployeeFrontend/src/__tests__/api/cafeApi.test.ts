import { setupServer } from 'msw/node';
import { rest } from 'msw';
import { renderHook } from '@testing-library/react-hooks';
import { Provider } from 'react-redux';
import { store } from '../../store';
import { useGetCafesQuery, useAddCafeMutation, useDeleteCafeMutation } from '../../api/cafeApi';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const server = setupServer(
  rest.get('http://localhost:5000/api/cafes', (req, res, ctx) => {
    return res(ctx.json([{ id: '1', name: 'Cafe 1', location: 'Location 1' }]));
  }),
  rest.post('http://localhost:5000/api/cafe', (req, res, ctx) => {
    return res(ctx.status(201));
  }),
  rest.delete('http://localhost:5000/api/cafe/:id', (req, res, ctx) => {
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

describe('cafeApi', () => {
  test('useGetCafesQuery', async () => {
    const { result, waitFor } = renderHook(() => useGetCafesQuery(''), { wrapper });

    await waitFor(() => result.current.isSuccess);

    expect(result.current.data).toEqual([{ id: '1', name: 'Cafe 1', location: 'Location 1' }]);
  });

  test('useAddCafeMutation', async () => {
    const { result, waitFor } = renderHook(() => useAddCafeMutation(), { wrapper });

    result.current[0]({ name: 'New Cafe', location: 'New Location' });

    await waitFor(() => result.current[1].isSuccess);

    expect(result.current[1].isSuccess).toBe(true);
  });

  test('useDeleteCafeMutation', async () => {
    const { result, waitFor } = renderHook(() => useDeleteCafeMutation(), { wrapper });

    result.current[0]('1');

    await waitFor(() => result.current[1].isSuccess);

    expect(result.current[1].isSuccess).toBe(true);
  });
});
