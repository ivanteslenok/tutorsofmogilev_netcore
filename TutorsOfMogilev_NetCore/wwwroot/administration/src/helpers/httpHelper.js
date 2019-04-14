import axios from 'axios';

export async function httpGet(url, successCB, failCB) {
  try {
    let response = await axios.get(url);

    successCB(await response.data);
  } catch (e) {
    failCB(e);
  }
}

export async function httpPost(url, data, successCB, failCB) {
  try {
    let response = await axios.post(url, data, {
      headers: {
        'Content-Type': 'application/json'
      }
    });

    successCB(await response.data);
  } catch (e) {
    failCB(e);
  }
}

export async function httpPut(url, data, successCB, failCB) {
  try {
    let response = await axios.put(url, data, {
      headers: {
        'Content-Type': 'application/json'
      }
    });

    successCB(await response.data);
  } catch (e) {
    failCB(e);
  }
}

export async function httpDelete(url, successCB, failCB) {
  try {
    let resp = await axios.delete(url);
    // TODO if добавлено для решения проблемы с CORS, можно удалить в релизе
    if (resp.config.method === 'delete') successCB();
  } catch (e) {
    failCB(e);
  }
}
