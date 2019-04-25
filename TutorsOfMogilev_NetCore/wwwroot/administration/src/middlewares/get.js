import { START, SUCCESS, FAIL } from '../constants';
import { httpGet } from '../helpers/httpHelper';

export default store => next => async action => {
  const { get, type, ...rest } = action;

  if (!get) return next(action);

  next({
    ...rest,
    type: type + START
  });

  try {
    const data = await httpGet(get);
    next({ ...rest, type: type + SUCCESS, data });
  } catch (error) {
    next({ ...rest, type: type + FAIL, error });
  }
};
