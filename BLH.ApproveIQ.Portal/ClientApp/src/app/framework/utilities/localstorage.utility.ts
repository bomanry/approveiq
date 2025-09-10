export function getItemWithExpiry(key: string): any | null {
  const itemStr = localStorage.getItem(key);
  if (!itemStr) return null;

  const item = JSON.parse(itemStr);
  const now = new Date();

  // Check if the item has expired
  if (now.getTime() > item.expiry) {
    localStorage.removeItem(key);
    return null;
  }

  return item.value;
}

export function setItemWithExpiry(key: string, value: any, expiryMinutes: number) {
  const now = new Date();
  const item = {
    value: value,
    expiry: now.getTime() + expiryMinutes * 60 * 1000, // Convert minutes to milliseconds
  };
  localStorage.setItem(key, JSON.stringify(item));
}
