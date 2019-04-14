import { START, SUCCESS, FAIL } from '../constants';
import { httpGet } from '../helpers/httpHelper';

export default store => next => action => {
  const { get, type, ...rest } = action;

  if (!get) return next(action);

  next({
    ...rest,
    type: type + START
  });

  httpGet(
    get,
    data => next({ ...rest, type: type + SUCCESS, data }),
    error => next({ ...rest, type: type + FAIL, error })
  );
};
