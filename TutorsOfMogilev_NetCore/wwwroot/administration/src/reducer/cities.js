import { CITY } from '../constants';
import reducerWithCrud from '../decorators/reducerWithCrud';

const cities = reducerWithCrud(CITY);
export default cities;
