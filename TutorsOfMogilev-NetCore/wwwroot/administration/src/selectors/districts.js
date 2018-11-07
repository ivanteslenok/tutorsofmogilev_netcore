import _ from 'lodash';
import { createSelector } from 'reselect';

const getDistricts = store => store.districts.items;

export const getSortedDistricts = createSelector(
  [getDistricts],
  (districts) => _.sortBy(districts, distr => distr['name'])
);
