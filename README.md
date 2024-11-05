# Koi Care System at Home

## Actors
- **Admin**
- **Member**

## Services

### Manage Koi
- Perform CRUD operations on Koi using `MemberID`.

### Manage Pond
- Perform CRUD operations on Pond using `MemberID`.

### Manage Water Parameters
- Check if water parameters exist using `WaterID` and `PondID`.
  - If they exist, open **Show Water**.
  - If not, open **Add Water**.

### Manage Food Calculation
- List ponds from `MemberID` and retrieve Koi from `PondID`.
- Calculate average age and size based on the selected food type.

### Manage Users
- Ban or unban users.
- Show notification when log in banned account.
