import { START, SUCCESS, FAIL } from '../constants';
import { httpPut } from '../helpers/httpHelper';

export default store => next => action => {
  const { put, type, payload, ...rest } = action;

  if (!put) return next(action);
  const { id, additionalUrl, data } = payload;

  next({
    ...rest,
    type: type + START
  });

  httpPut(
    `${put}/${id}${additionalUrl || ''}`,
    data,
    data => next({ ...rest, type: type + SUCCESS, data, payload }),
    error => next({ ...rest, type: type + FAIL, error })
  );
};
