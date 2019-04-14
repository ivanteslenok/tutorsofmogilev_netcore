import { GRID_STATE_CHANGE_ACTION } from '../constants';

export const createGridAction = (partialStateName, partialStateValue) => ({
  type: GRID_STATE_CHANGE_ACTION,
  partialStateName,
  partialStateValue
});
