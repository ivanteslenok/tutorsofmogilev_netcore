import { START, SUCCESS, FAIL } from '../constants';
import { httpDelete } from '../helpers/httpHelper';

export default store => next => async action => {
  const { del, type, payload, ...rest } = action;

  if (!del) return next(action);

  next({
    ...rest,
    type: type + START
  });

  try {
    await httpDelete(`${del}/${payload.id}`);
    next({ ...rest, type: type + SUCCESS, payload });
  } catch (error) {
    next({ ...rest, type: type + FAIL, error });
  }
};
