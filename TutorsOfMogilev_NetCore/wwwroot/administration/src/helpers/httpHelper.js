import axios from 'axios';

export async function httpGet(url) {
  const response = await axios.get(url);

  return await response.data;
}

export async function httpPost(url, data) {
  const response = await axios.post(url, data, {
    headers: {
      'Content-Type': 'application/json'
    }
  });

  return await response.data;
}

export async function httpPut(url, data) {
  const response = await axios.put(url, data, {
    headers: {
      'Content-Type': 'application/json'
    }
  });

  return await response.data;
}

export async function httpDelete(url) {
  const response = await axios.delete(url);
  
  // TODO if добавлено для решения проблемы с CORS, можно удалить в релизе
  if (response.config.method === 'delete') return;
}
