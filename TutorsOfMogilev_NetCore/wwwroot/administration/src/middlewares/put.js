import { START, SUCCESS, FAIL } from '../constants';
import { httpPut } from '../helpers/httpHelper';

export default store => next => async action => {
  const { put, type, payload, ...rest } = action;

  if (!put) return next(action);
  const { id, additionalUrl } = payload;

  next({
    ...rest,
    type: type + START
  });

  try {
    const data = await httpPut(`${put}/${id}${additionalUrl || ''}`, payload.data);
    next({ ...rest, type: type + SUCCESS, data, payload });
  } catch (error) {
    next({ ...rest, type: type + FAIL, error });
  }
};
