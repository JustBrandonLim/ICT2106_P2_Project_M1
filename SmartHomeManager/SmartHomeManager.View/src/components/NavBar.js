import {React, useState} from "react";
import {
    Avatar,
    Box,
    Button,
    Collapse,
    Flex,
    IconButton,
    Link,
    Menu,
    MenuButton,
    MenuDivider,
    MenuItem,
    MenuList,
    Stack,
    Text,
    useBreakpointValue,
    useColorModeValue,
    useDisclosure
} from "@chakra-ui/react";

import {CloseIcon, HamburgerIcon} from "@chakra-ui/icons";

import {Link as RouterLink, useNavigate} from "react-router-dom";

import Notification from "components/Notification/Notification";

export function NavBar() {

    const [accountUsername, updateAccountUsername] = useState(localStorage.getItem('username'))

    //Navigation declaration
    const navigate = useNavigate()

    const signOut = () => {
        localStorage.clear()
        updateAccountUsername(null)
        navigate("/", {replace: true});
    }

    const {isOpen, onToggle} = useDisclosure();
    return (
        <Box>
            <Flex
                bg={useColorModeValue("white", "gray.800")}
                color={useColorModeValue("gray.600", "white")}
                minH={"60px"}
                py={{base: 2}}
                px={{base: 4}}
                borderBottom={1}
                borderStyle={"solid"}
                borderColor={useColorModeValue("gray.200", "gray.900")}
                align={"center"}
            >
                <Flex flex={{base: 1, md: "auto"}} ml={{base: -2}} display={{base: "flex", md: "none"}}>
                    <IconButton
                        onClick={onToggle}
                        icon={isOpen ? <CloseIcon w={3} h={3}/> : <HamburgerIcon w={5} h={5}/>}
                        variant={"ghost"}
                        aria-label={"Toggle Navigation"}
                    />
                </Flex>
                <Flex flex={{base: 1}} justify={{base: "center", md: "start"}}>
                    <Text textAlign={useBreakpointValue({base: "center", md: "left"})} fontFamily={"heading"}
                          color={useColorModeValue("gray.800", "white")}>
                        Logo
                    </Text>

                    <Flex display={{base: "none", md: "flex"}} ml={10}>
                        <DesktopNav/>
                    </Flex>
                </Flex>

                <Notification/>

                {accountUsername != null ?

                    <Menu>
                        <MenuButton
                            as={Button}
                            rounded={'full'}
                            variant={'link'}
                            cursor={'pointer'}
                            minW={0}>
                            <Avatar
                                size={'sm'}

                            />
                        </MenuButton>
                        <MenuList>
                            <MenuItem as={RouterLink}
                                      to="/myaccount">My Account</MenuItem>
                            <MenuItem as={RouterLink}
                                      to="/profiles">Profiles</MenuItem>
                            <MenuDivider/>
                            <MenuItem onClick={() => signOut()}>Sign out</MenuItem>
                        </MenuList>
                    </Menu>
                    :
                    <Stack flex={{base: 1, md: 0}} justify={"flex-end"} direction={"row"} spacing={6}>
                        <Button fontSize={"sm"} fontWeight={400} variant={"link"} as={RouterLink} to="/login">
                            Sign In
                        </Button>
                        <Button
                            as={RouterLink}
                            to="/register"
                            display={{base: "none", md: "inline-flex"}}
                            fontSize={"sm"}
                            fontWeight={600}
                            color={"white"}
                            bg={"pink.400"}
                            _hover={{
                                bg: "pink.300",
                            }}
                        >
                            Sign Up
                        </Button>

                    </Stack>
                }
            </Flex>

            <Collapse in={isOpen} animateOpacity>
                <MobileNav/>
            </Collapse>
        </Box>
    );
}

const DesktopNav = () => {
    const linkColor = useColorModeValue("gray.600", "gray.200");
    const linkHoverColor = useColorModeValue("gray.800", "white");

    return (
        <Stack direction={"row"} spacing={4}>
            {NAV_ITEMS.map((navItem) => (
                <Box key={navItem.label}>
                    <RouterLink
                        p={2}
                        to={navItem.href ?? "#"}
                        fontSize={"sm"}
                        fontWeight={500}
                        color={linkColor}
                        _hover={{
                            textDecoration: "none",
                            color: linkHoverColor,
                        }}
                        as={Link}
                    >
                        {navItem.label}
                    </RouterLink>
                </Box>
            ))}
        </Stack>
    );
};

const MobileNav = () => {
    return (
        <Stack bg={useColorModeValue("white", "gray.800")} p={4} display={{md: "none"}}>
            {NAV_ITEMS.map((navItem) => (
                <MobileNavItem key={navItem.label} {...navItem} />
            ))}
        </Stack>
    );
};

const MobileNavItem = ({label, href}) => {
    return (
        <Stack spacing={4}>
            <Flex
                py={2}
                as={Link}
                href={href ?? "#"}
                justify={"space-between"}
                align={"center"}
                _hover={{
                    textDecoration: "none",
                }}
            >
                <Text fontWeight={600} color={useColorModeValue("gray.600", "gray.200")}>
                    {label}
                </Text>
            </Flex>
        </Stack>
    );
};

const NAV_ITEMS = [
    {
        label: "Home",
        href: "/",
    },
    {
        label: "Devices",
        href: "/devices",
    },
    {
        label: "Manage Devices",
        href: "/managedevices",
    },
    {
        label: "Register Device",
        href: "/selectnearbydevice",
    },
    {
        label: "Profiles",
        href: "/profiles",
    },
    {
        label: "Scenario",
        href: "/Scenario",
    },
    {
        label: "Rooms",
        href: "/rooms",
    },
    {
        label: "Device Config",
        href: "/config",
    },
    {
        label: "Analytics",
        href: "/analytics",
    },
    {
        label: "Shared Platform",
        href: "/sharedplatform",
    },
];
