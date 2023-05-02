package espeon.auth.jade;

public enum UserLevel {
	Anonymous,
	User,
	Test,
	Mod,
	Admin,
	Creator;
	public int id() {
		return ordinal();
	}
	public static final String name_anonymous = "Anonymous";
	public static final String name_user = "User";
	public static final String name_test = "Test";
	public static final String name_mod = "Mod";
	public static final String name_admin = "Admin";
	public static final String name_creator = "Creator";
}